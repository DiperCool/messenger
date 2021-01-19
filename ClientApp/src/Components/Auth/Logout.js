import React from 'react';
import {AuthApi} from '../../Api/Auth/Auth';

export const Logout = () => {


    let onClick=()=>{
        AuthApi.logout()
    }

    return ( 
        <div>
            <button onClick={onClick}>Выйти</button>
        </div>
    );
}
 