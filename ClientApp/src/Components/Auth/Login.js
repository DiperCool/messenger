import React,{useRef, useState, useContext} from "react";
import {Redirect} from "react-router-dom";
import {AuthApi} from "../../Api/Auth/Auth";
import {UserContext} from "../UserComponent/UserContext";

export const Login=()=>{
    let [isOk, setOk]= useState(false);
    let [, setErrors]=useState({
        isErrors:false,
        allErrors:[]
    });
    let refLogin= useRef(null);
    let refPassword=useRef(null);

    let {setGetLogin}= useContext(UserContext);


    const handler=async()=>{
        let login= refLogin.current.value;
        let password= refPassword.current.value;
        let result=await AuthApi.login(login,password);
        if(result.notSuccesed){
            setErrors({
                isErrors:true,
                allErrors:[result.errors]
            })
            return;
        }
        setGetLogin()
        setOk(true);
    }

    if(isOk){
        return <Redirect to="/profile"/>
    }

    return (
        <div>
            <div>
                <input 
                    ref={refLogin}
                    type="text" 
                    id="standard-basic" 
                        abel="Login" />
                <br></br>
                <input 
                    ref={refPassword} 
                    id="standard-basic" 
                    label="Password" 
                    type="password"
                        />
                <br></br>
                <button onClick={handler}>Войти</button>
            </div>
        </div>
    )
}