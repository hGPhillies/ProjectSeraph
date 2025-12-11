//REFACTOR
type Nullable<T> = T | undefined | null;
class User {
    username: string | null | undefined;
    password: string | null | undefined;
    id: Nullable<string>;
    nurse: boolean;
    loggedIn: boolean;

    constructor(isNurse: boolean, LoggedIn: boolean) {
        this.nurse = isNurse;
        this.loggedIn = LoggedIn;
    }
}

export default User;