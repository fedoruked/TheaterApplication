insert into users(email, password, approve_code, approved, created, updated)
values(
	'admin@gmail.com'
	, '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8'
	, '05a06dce-3b32-40e6-9f1b-92794687ae3f'
	, now() at time zone 'utc'
	, now() at time zone 'utc'
	, now() at time zone 'utc'
),
(
	'client@gmail.com'
	, '5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8'
	, '05a06dce-3b32-40e6-9f1b-92794687ae3f'
	, now() at time zone 'utc'
	, now() at time zone 'utc'
	, now() at time zone 'utc'
);

insert into user_roles(user_id, role_id, created, updated)
select u.id, 1, now() at time zone 'utc', now() at time zone 'utc'
from users u
where u.email = 'admin@gmail.com';

insert into user_roles(user_id, role_id, created, updated)
select u.id, 2, now() at time zone 'utc', now() at time zone 'utc'
from users u
where u.email = 'client@gmail.com';

insert into performances(name, duration_minutes, created, updated)
values ('The Great Gatsby', 90, now() at time zone 'utc', now() at time zone 'utc')
, ('Magic Goes Wrong', 120, now() at time zone 'utc', now() at time zone 'utc')
, ('The Woman In Black', 50, now() at time zone 'utc', now() at time zone 'utc');

insert into performance_schedules(performance_id, start_at, tickets_count, is_repeat, created, updated)
select p.id
	, '2020-11-01 12:00'
	, 120
	, true
	, now() at time zone 'utc'
	, now() at time zone 'utc'
from performances p
where name = 'The Great Gatsby';

insert into performance_schedules(performance_id, start_at, tickets_count, is_repeat, created, updated)
select p.id
	, '2020-11-03 16:00'
	, 120
	, true
	, now() at time zone 'utc'
	, now() at time zone 'utc'
from performances p
where name = 'The Great Gatsby';

insert into performance_schedules(performance_id, start_at, tickets_count, is_repeat, created, updated)
select p.id
	, '2020-11-10 19:00'
	, 200
	, false
	, now() at time zone 'utc'
	, now() at time zone 'utc'
from performances p
where name = 'The Great Gatsby';

insert into performance_schedules(performance_id, start_at, tickets_count, is_repeat, created, updated)
select p.id
	, '2020-11-05 09:00'
	, 50
	, true
	, now() at time zone 'utc'
	, now() at time zone 'utc'
from performances p
where name = 'Magic Goes Wrong';

insert into performance_schedules(performance_id, start_at, tickets_count, is_repeat, created, updated)
select p.id
	, '2020-11-10 16:00'
	, 200
	, false
	, now() at time zone 'utc'
	, now() at time zone 'utc'
from performances p
where name = 'The Woman In Black';
