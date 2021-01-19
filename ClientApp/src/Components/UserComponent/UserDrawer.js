import React, { useContext } from "react";
import Avatar from '@material-ui/core/Avatar';
import { UserContext } from "./UserContext";
export const UserDrawer=()=>{

    let {Auth}=useContext(UserContext);


    return(
        <div>
            {Auth.login}
            <Avatar src={Auth.ava}/>
        </div>
    )
}