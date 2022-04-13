import React from 'react';
import Landing from '../pages/landing/Landing';
import { Plane, Quiz } from '../pages/quiz/Quiz';
import Result from '../pages/result/Result';
import './App.css';

interface AppState {
  windowState: WindowState,
  plane?: Plane
}

enum WindowState {
  Landing,
  Quiz,
  Result
}

export default class App extends React.Component<{}, AppState> {
  constructor(props: {}) {
    super(props);
    this.state = {
      windowState: WindowState.Landing
    };
  }

  landingClickHandler() {
    this.setState({windowState: WindowState.Quiz});
  }


  calculateResult(plane: Plane) {
    this.setState({
      windowState: WindowState.Result,
      plane: plane
    });
  }

  render(): React.ReactNode {
    const window = this.state.windowState;

    switch (window) {
      case WindowState.Landing:
        return <Landing clickHandler={this.landingClickHandler.bind(this)}/>;
      case WindowState.Quiz:
        return <Quiz finishCallback={this.calculateResult.bind(this)}/>;
      case WindowState.Result:
        return this.state.plane ? <Result plane={this.state.plane}/> : <p>Plane is null</p>;
    }
  }
}
