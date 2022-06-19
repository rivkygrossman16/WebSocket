import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './Layout';
import Login from './Pages/Login';
import Signup from './Pages/Signup';
import { AuthContextComponent } from './AuthContext';
import Logout from './Pages/Logout';
import Home from './Pages/Home';
import PrivateRoute from './Components/PrivateRoute';



export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <AuthContextComponent>
                <Layout>
                    <Route exact path='/signup' component={Signup} />
                    <Route exact path='/login' component={Login} />
                    <Route exact path='/logout' component={Logout} />
                    <PrivateRoute exact path='/' component={Home} />
                </Layout>
            </AuthContextComponent>
        );
    }
}