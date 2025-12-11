import { createContext, useContext, useState, ReactNode } from 'react';
import type User from '../domain/User.ts';

//An authorized user is a domain user without the password, to prevent the password being 'visible' in the app.
//skal paa sigt ogsaa have omitted username - ikke relevant naar vi har userid
export type AuthorizedUser = Omit<User, 'password'>;

//defines the type to be passed to the usercontext.provider.
interface UserContextType {
    user: AuthorizedUser;
    setUser: (user: AuthorizedUser) => void;
}

//defines 'before user is logged in', looks like this:
const initialUser: AuthorizedUser = {
    id: null,
    username: null,
    loggedIn: false,
    nurse: false,
};

//creates the context we pass to the app to use
const UserContext = createContext<UserContextType | undefined>(undefined)

//component som definerer vores UserProvider, som giver den reelle userstate til appen
export function UserProvider({ children }: { children: ReactNode }) {
    const [user, setUser] = useState<AuthorizedUser>(initialUser);

    return (
        <UserContext.Provider value= { { user, setUser }}>
            { children }
        </UserContext.Provider>
        );
}

//defining a custom hook. the custom hook allows us to avoid writing hook and 
//error checking in every component needing the authorized user instance
export function userContext() {
    const context = useContext(UserContext);
    if (context === undefined) {
        throw new ScopeError('userContext hook must be used within a UserProvider')
    }

    return context;
}