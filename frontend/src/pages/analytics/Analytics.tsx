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
    totalUsers : number;
}

interface AuthLeft{
    activeUsers : number;
    totalChatMessages : number;
    totalUsers : number;
}

interface LibLeft{
    activeUsers : number;
    totalChatMessages : number;
    totalUsers : number;
}

interface LibRight{
    activeUsers : number;
    totalChatMessages : number;
    totalUsers : number;
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

        console.log(this.state.data?.chatAnalytics.AuthLeft?.totalUsers)


        return (
            <>
            <div className="">

                <div className="justify-center flex p-10 whitespace-pre-line">
                    <h1 className="  text-3xl font-bold ">{"Admin Panel"}</h1>
                </div>

                <div className="justify-center flex pb-10">
                        API call amount:  {this.state.data?.apiAnalytics.totalApiCalls}
                </div>

                <div className="justify-center flex pb-10">
                        Total Quizes Evaluated:  {this.state.data?.evaluationAnalytics}
                </div>

                <div className="grid grid-cols-4 gap-5 p-5">                    

                    <div className="flex justify-center border-2 border-red-300 rounded p-6 bg-red-200">
                        AuthLeft Statistics: <br />
                        Active AuthLeft Users: {this.state.data?.chatAnalytics.AuthLeft?.activeUsers} <br />
                        Total AuthLeft Messages: {this.state.data?.chatAnalytics.AuthLeft?.totalChatMessages} <br />
                        Total AuthLeft Users: {this.state.data?.chatAnalytics.AuthLeft?.totalUsers}      
                    </div>
                    <div className="flex justify-center border-2 border-blue-300 rounded p-6 bg-blue-200">
                        AuthRight Statistics: <br />
                        Active AuthRight Users: {this.state.data?.chatAnalytics.AuthRight?.activeUsers} <br />
                        Total AuthRight Messages: {this.state.data?.chatAnalytics.AuthRight?.totalChatMessages} <br />
                        Total AuthRight Users: {this.state.data?.chatAnalytics.AuthRight?.totalUsers}                   
                    </div>
                    <div className="flex justify-center border-2 border-green-300 rounded p-6 bg-green-200">
                        LibLeft Statistics: <br />
                        Active LibLeft Users: {this.state.data?.chatAnalytics.LibLeft?.activeUsers} <br />
                        Total LibLeft Messages: {this.state.data?.chatAnalytics.LibLeft?.totalChatMessages} <br />
                        Total LibLeft Users: {this.state.data?.chatAnalytics.LibLeft?.totalUsers}                     
                    </div>
                    <div className="flex justify-center border-2 border-yellow-300 rounded p-6 bg-yellow-200">
                        LibRight Statistics: <br />
                        Active LibRight Users: {this.state.data?.chatAnalytics.LibRight?.activeUsers} <br />
                        Total LibRight Messages: {this.state.data?.chatAnalytics.LibRight?.totalChatMessages} <br />
                        Total LibRight Users: {this.state.data?.chatAnalytics.LibRight?.totalUsers}                  
                    </div>

                </div>

                <div className="flex justify-center pt-10">
                    <button onClick={() => this.componentDidMount()}
                            className="bg-gray-400 text-white font-bold py-2 px-4 rounded">
                        Refresh Statistics
                    </button>
                </div>

                <div className="fixed inset-x-0 bottom-0 p-8 bg-gray-300 justify-center flex">
                
                </div>

            </div>
            </>
        );
    }
}
