import React from "react";
import axios from "axios";
import { getUrl } from "../../config/Constants";
import { stat } from "fs";

interface AnalyticsProps {

}

interface Result {
    x: number;
    y: number;
}

interface AnalyticsData {
    //chatAnalytic
    chatAnalytics : chatAnalytics;
    //apiAnalytics
    apiAnalytics : apiAnalytics;
    //Times Evalueted
    evaluationAnalytics : number;
}

interface chatAnalytics{
    AuthRight? : AuthRight;
    LibRight? : LibRight;
    AuthLeft? : AuthLeft;
    LibLeft? : LibLeft;
}

interface AuthRight{
    activeUsers : number;
    totalChatMessages : number;
}

interface AuthLeft{
    activeUsers : number;
    totalChatMessages : number;
}

interface LibLeft{
    activeUsers : number;
    totalChatMessages : number;
}

interface LibRight{
    activeUsers : number;
    totalChatMessages : number;
}

interface apiAnalytics{
    totalApiCalls? : number;
}

interface AnalyticsState {
    data?: AnalyticsData;
}



export class Analytics extends React.Component<AnalyticsProps, AnalyticsState> {
    constructor(props: AnalyticsProps) {
        super(props);
        this.state = {}
    }

    
    componentDidMount() {    
        axios.get(getUrl("/analytics"),
        {
            headers: {"Authorization" : `Bearer ${localStorage.getItem("jwtToken")}`}
        })
        .then((response) => {
            console.log("RESPONDED")
            this.setState({
                data : response.data as AnalyticsData
            });
        })
    }

    totalChatters(){
        //TODO
    }





    render(): React.ReactNode {
        const loadIfNull = (value?: any | undefined): string => {
            return value ? value.toString() : "Loading...";

        };


        console.log(this.state.data)


        return (
            <>
            <div>

                <p>Quizes have benn evaluated: {this.state.data?.evaluationAnalytics}</p>

                <div>
                    <p>AuthLeft Statistics: </p>
                    Active AuthLeft Users: {this.state.data?.chatAnalytics.AuthLeft?.activeUsers} <br />
                    Total AuthLeft Messages: {this.state.data?.chatAnalytics.AuthLeft?.totalChatMessages}                    
                </div>
                <br />
                <div>
                    <p>AuthRight Statistics: </p>
                    Active AuthRight Users: {this.state.data?.chatAnalytics.AuthRight?.activeUsers} <br />
                    Total AuthRight Messages: {this.state.data?.chatAnalytics.AuthRight?.totalChatMessages}                    
                </div>
                <br />
                <div>
                    <p>LibLeft Statistics: </p>
                    Active LibLeft Users: {this.state.data?.chatAnalytics.LibLeft?.activeUsers} <br />
                    Total LibLeft Messages: {this.state.data?.chatAnalytics.LibLeft?.totalChatMessages}                    
                </div>
                <br />
                <div>
                    <p>LibRight Statistics: </p>
                    Active LibRight Users: {this.state.data?.chatAnalytics.LibRight?.activeUsers} <br />
                    Total LibRight Messages: {this.state.data?.chatAnalytics.LibRight?.totalChatMessages}                    
                </div>

            </div>

            </>
        );
    }
}
