export class LoginModel{
    statusCode: Number;
    isSuccess: boolean;
    errorMessage: [];
    result: {
      user: {
        userId: Number,
        pilotID: Number,
        userEmail: any,
        userName: string
        password: string,
        role: string
      },
      Token: string
    }
  }