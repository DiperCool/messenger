import React,{useContext} from 'react';
import { Route, Redirect } from 'react-router-dom';
import { UserContext } from '../UserComponent/UserContext';
export const PrivateRoute = ({component: Component, ...rest}) => {
    let {Auth}= useContext(UserContext);
    return (
        <Route {...rest} render={props => (
            Auth.isAuth ?
            <Component {...props}/>
            : <Redirect to={{
                pathname:"/login",
                state:{from:props.location}
            }} />
        )} />
    );
};