class ScopeError extends Error {
    public name: string = "ScopeError";

    constructor(message: string) {
        super(message)
    }
}

export default ScopeError;