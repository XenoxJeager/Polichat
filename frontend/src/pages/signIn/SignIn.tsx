import axios from "axios";
import React from "react";
import { NavigateFunction, useNavigate } from "react-router";
import { getUrl } from "../../config/Constants";

interface SignInProps {
    navigate?: NavigateFunction;
}

interface SignInState {
    username: string;

    // aint no fucks given about security
    password: string;
}

class SignIn extends React.Component<SignInProps, SignInState> {
    submitLogin() {
        axios.post(getUrl("/getJWT"), 
        {
            username: this.state.username, 
            password: this.state.password
        })
        .then((response) => {
            var jwt = response.data.jwt as string;
            localStorage.setItem("JWT", jwt);
            this.props.navigate!("analytics");
        });
    }

    render(): React.ReactNode {
        return (
            <>
                <p>Username: </p>
                <input onChange={(event) => this.setState({username: event.target.value})}></input>
                <p>Password: </p>
                <input onChange={(event) => this.setState({password: event.target.value})}></input>
                <button onClick={this.submitLogin.bind(this)}>Submit</button>
            </>
        );
    }
}

export const WrappedSignIn = () => {
    return <SignIn navigate={useNavigate()}/>;
};
