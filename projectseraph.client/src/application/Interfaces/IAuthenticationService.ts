import type { LoginCredentials } from '../DTOs/LoginCredentialsDTO.ts';
import type { AuthorizedUser } from '../UserContext.tsx';

export interface IAuthenticationService {
    authenticateUser(userCredentials: LoginCredentials): Promise<AuthorizedUser>;
}