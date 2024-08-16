import React from 'react';
import ReactDOM from 'react-dom/client';
import { App } from './App';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import AppCustom from './AppCustom';

const root = ReactDOM.createRoot(document.getElementById('root')!);
root.render(
  <React.StrictMode>
    <BrowserRouter>
      {/* <App /> */}
      <AppCustom />
    </BrowserRouter>
  </React.StrictMode>
);
