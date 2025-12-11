import { type IAuthenticationService } from '../Interfaces/IAuthenticationService.ts';

export function AuthenticateUser(authSer: IAuthenticationService) {
    return async (username: string, password: string) => {
        //create the object to send to the api
        const c = { username, password };

        const user = await authSer.authenticateUser(c);

        return user;
    };
}