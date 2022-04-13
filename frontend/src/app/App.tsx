import React from 'react';
import { BrowserRouter, Link } from 'react-router-dom';
import { Route, Router, Routes } from 'react-router';
import Landing from '../pages/landing/Landing';
import { Plane, Quiz } from '../pages/quiz/Quiz';
import Result from '../pages/result/Result';
import './App.css';
import { NoPlane } from '../pages/errors/noPlane/NoPlane';
import { NotFound } from '../pages/errors/notFound/NotFound';
import { Analytics } from '../pages/analytics/Analytics';

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
    return (
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Landing clickHandler={this.landingClickHandler} />} />
          <Route path='quiz' element={<Quiz finishCallback={this.landingClickHandler}/>} />
          <Route path='result' element={this.state.plane ? <Result plane={this.state.plane}/> : <NoPlane />} />
          <Route path='analytics' element={<Analytics />} />
          
          <Route path='*' element={<NotFound />} />
        </Routes>
      </BrowserRouter>
    );

    /*
    const window = this.state.windowState;

    switch (window) {
      case WindowState.Landing:
        return <Landing clickHandler={this.landingClickHandler.bind(this)}/>;
      case WindowState.Quiz:
        return <Quiz finishCallback={this.calculateResult.bind(this)}/>;
      case WindowState.Result:
        return this.state.plane ? <Result plane={this.state.plane}/> : <p>Plane is null</p>;
    } 
    */
  }
}
