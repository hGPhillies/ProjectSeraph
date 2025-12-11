import axios from 'axios';
import type { LoginCredentials } from '../application/DTOs/LoginCredentialsDTO';
import type { AuthorizedUser } from '../application/UserContext.tsx';
// if more endpointcalls added, needs to be changed, moving last part to specific call
const BASE_URL = "https://localhost:5001/auth/user";

class AuthenticationService {
    static async authenticateUser(userCredentials: LoginCredentials): Promise<AuthorizedUser> {
        //could be refactored to adhere to the authorized user type - for type safety
        const authorizedUser = await axios.post(BASE_URL, userCredentials)

        return authorizedUser.data as AuthorizedUser;
    }
}

export default AuthenticationService;