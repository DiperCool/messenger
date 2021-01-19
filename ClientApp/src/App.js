import React from 'react';
import { BrowserRouter, Switch, Redirect } from 'react-router-dom';
import {Login} from "./Components/Auth/Login"
import { Register } from './Components/Auth/Register';
import {User} from "./Components/UserComponent/User"
import {PublicRoute} from "./Components/tools/PublicRoute";
import { PrivateRoute } from './Components/tools/PrivateRoute';
import { MenuComponent } from './Components/MenuComponent/MenuComponent';
import { ChatsComponent } from './Components/ChatComponent/ChatsComponent';
export const App=()=>{

  return(
    <User>
      <MenuComponent>
        <BrowserRouter>
          <Switch>

            <PublicRoute exact path="/login" component={Login}/>
            <PublicRoute exact path="/register" component={Register}/>

            <PrivateRoute exact path="/" component={ChatsComponent}/>

            <Redirect to={"/get"}/>
          </Switch>
        </BrowserRouter>
      </MenuComponent>
    </User>
  )
}