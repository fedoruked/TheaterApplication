export class DataWithPaging<T> {
  public page: number;
  public totalPages: number;
  public totalCount: number;
  public data: T[];
}
