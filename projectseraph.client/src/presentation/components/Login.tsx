import { useState } from "react";
import { useNavigate } from 'react-router-dom';
import AuthenticationService from "../../services/AuthenticationService";
import { userContext, type AuthorizedUser } from "../../application/UserContext.tsx";
import { AuthenticateUser } from '../../application//UseCases/AuthenticateUser.ts';

function Login() {
    const { setUser } = userContext();
    const [username, setUsername] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const [loading, setLoading] = useState<boolean>(false)
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState<boolean>(false);

    const navigate = useNavigate();

    // Validation - check if all inputfields have value
    // Used to disable/enable submit-button
    const isFormValid =
        username.trim() !== "" &&
        password.trim() !== "";

    const handleLogin = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        if (!isFormValid) return;

        setLoading(true);

        try {
            const loginUseCase = AuthenticateUser(AuthenticationService);
            const seraphServerResponse = await loginUseCase(username, password);

            const authenticatedUser: AuthorizedUser = {
                ...seraphServerResponse,
                loggedIn: true,
            };
            setUser(authenticatedUser);

            setSuccess(true);
            navigate("/");
        } catch {
            setError("Noget gik galt ved login, proev igen.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <form onSubmit={handleLogin}>
                <table>
                    <tbody>
                        <tr>
                            <td>
                                <label htmlFor="username">CPR-nummer:</label>
                            </td>
                            <td>
                                <input
                                    name="username"
                                    id="username"
                                    type="text"
                                    value={username}
                                    onChange={(e) => setUsername(e.target.value)}
                                    required
                                />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label htmlFor="password">Adgangskode:</label>
                            </td>
                            <td>
                                <input
                                    name="password"
                                    id="password"
                                    type="password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    required
                                />
                            </td>
                        </tr>
                    </tbody>
                </table>

                <br />
                {/* Mini-spinner */}
                <button type="submit" disabled={!isFormValid || loading}>
                    {loading
                        ? "Vent..."
                        : "Log ind"}
                </button>
            </form>

            {/* Feedback under form */}
            {error && <div style={{ color: "red" }}>{error}</div>}
            {success && <div style={{ color: "green" }}>Blodtryksmåling sendt!</div>}
        </div>
    );
}

export default Login;
