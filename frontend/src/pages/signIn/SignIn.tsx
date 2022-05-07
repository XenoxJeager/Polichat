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
    constructor(props: SignInProps) {
        super(props);

        this.state = {
            username: "",
            password: ""
        };
    }

    submitLogin() {
        axios.post(getUrl("/signIn"),
        {
            username: this.state.username,
            password: this.state.password
        })
        .then((response) => {
            if (response.status !== 200)
                return;
                
            localStorage.setItem("jwtToken", response.data);
            this.props.navigate!("/analytics");
        }).catch((ex) => {
            console.log(ex);
        });
    }

    render(): React.ReactNode {
        return (
            <>
                <div className="flex items-center justify-center min-h-screen bg-gray-20">
                    <div className="px-8 py-6 mt-4 text-left bg-white shadow-lg">
                        <h3 className="text-2xl font-bold text-center">Sign In</h3>
                        <div>
                            <div className="mt-4">
                                <div>
                                    <label>Username</label>
                                    <input type="text" placeholder="Username" 
                                    className="w-full px-4 py-2 mt-2 rounded-5 bg-gray-50"
                                    onChange={(event) => this.setState({username: event.target.value})}
                                    />
                                </div>
                                <div className="mt-4">
                                    <label>Password</label>
                                    <input type="text" placeholder="Password" 
                                    className="w-full px-4 py-2 mt-2 rounded-5 bg-gray-50"
                                    onChange={(event) => this.setState({password: event.target.value})}
                                    />
                                </div>
                                    <button onClick={this.submitLogin.bind(this)}
                                    className="justify-center flex px-6 py-2 mt-4 text-white bg-blue-600 rounded-lg hover:bg-blue-900"
                                    >Submit</button>
                            </div>
                        </div> 
                    </div>
                </div>
                
            </>
        );
    }
}

export const WrappedSignIn = (props: SignInProps) => {
    return <SignIn {...props} navigate={useNavigate()}/>;
};
