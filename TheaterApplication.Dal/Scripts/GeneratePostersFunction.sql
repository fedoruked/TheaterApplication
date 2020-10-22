create or replace function generate_posters(
	from_date 						timestamp with time zone
	, to_date 						timestamp with time zone
)
returns table (
	id 								int
	, difference_from_start_days 	int
	, event_date 					timestamp without time zone
	, schedule_id 					int
	, created 						timestamp without time zone
	, updated 						timestamp without time zone
	, booked_count					bigint
)
as $$
begin 
	return query (
		with recursive cte_schedules(
			id
			, performance_id
			, start_at
			, difference_from_start_days
			, is_repeat
		) as (
			select 
				ps.id
				, ps.performance_id 
				, ps.start_at
				, 0 as difference_from_start_days
				, ps.is_repeat 
			from performance_schedules ps 
			
			union all 
			
			select 
				cs.id
				, cs.performance_id 
				, cs.start_at + interval '7 days'
				, cs.difference_from_start_days + 7 as difference_from_start_days
				, cs.is_repeat
			from cte_schedules cs
			where cs.start_at < to_date
			and cs.is_repeat = true
		)
	
		select 
			case when pp.id is null then 0 else pp.id end 	as id
			, cs.difference_from_start_days
			, cs.start_at 									as event_date
			, cs.id											as schedule_id
			, now() at time zone 'utc'						as created
			, now() at time zone 'utc'						as updated
			, case 
				when pb.booked_count is null 
				then 0 
				else pb.booked_count 
			end 											as booked_count
		from cte_schedules cs
		left join performance_posters pp 
		on pp.schedule_id  = cs.id
		and pp.difference_from_start_days  = cs.difference_from_start_days
		left join (
			select 
				pb.poster_id
				, count(pb.id) as booked_count
			from performance_bookings pb 
			group by pb.poster_id
		) as pb
		on pb.poster_id = pp.id
		where cs.start_at > from_date
	);
	
	
	
end; 
$$ language plpgsql