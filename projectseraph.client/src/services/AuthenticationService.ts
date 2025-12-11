import type { LoginCredentials } from "../application/DTOs/LoginCredentialsDTO";

class AuthenticationService {
    static async authenticateLogin(userCredentials: LoginCredentials): Promise<AuthorizedUser> {

    }
}

export default AuthenticationService;