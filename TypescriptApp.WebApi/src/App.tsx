import React from 'react';
import './App.scss';
import { MainRouter } from './mainRouter';

const App: React.FC = () => {
  return (
    <div className="App">
      <div className='content-container'>
        <MainRouter />
      </div>
    </div>
  );
}

export default App;
