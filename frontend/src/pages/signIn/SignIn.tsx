import React from "react";
import { NavigateFunction, useNavigate } from "react-router";

interface SignInProps {
    navigate?: NavigateFunction;
}

interface SignInState {

}

class SignIn extends React.Component<SignInProps> {
    render(): React.ReactNode {
        return (
            <>
                <p>Username: </p>
                <input></input>
                <p>Password: </p>
                <input></input>
            </>
        );
    }
}

export const WrappedSignIn = () => {
    return <SignIn navigate={useNavigate()}/>;
};
