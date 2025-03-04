import React from 'react';
import Nav from '../components/Nav';
import { Outlet } from 'react-router-dom';

const AppLayout: React.FC = () => {
  return (
    <div>
      <Nav />
      <main>
        <Outlet />
      </main>
    </div>
  );
};

export default AppLayout;