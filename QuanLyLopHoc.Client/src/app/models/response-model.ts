export interface ResponseModel<T>{
  message: string;
  result: T;
  isSucceed: boolean;
}
