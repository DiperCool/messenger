import React, {useRef, useState, useContext} from "react";
import {AuthApi} from "../../Api/Auth/Auth";
import { UserContext } from "../UserComponent/UserContext";
export const Register=()=>{

    let [, setErrors]=useState({
        isErrors:false,
        allErrors:[]
    });
    let {setGetLogin}= useContext(UserContext);
    let refLogin = useRef(null);
    let refPassword = useRef(null);
    let refRePassword = useRef(null);

    let handler=async()=>{
        let login=refLogin.current.value;
        let pas=refPassword.current.value;
        let rePas=refRePassword.current.value;
        let result= await AuthApi.register(login,pas,rePas);
        if(result.notSuccesed){
            setErrors({
                isErrors:true,
                allErrors:result.errors
            })
            return;
        }
        setGetLogin();



    }

    return(
            <div>
                 <div>
                    <input type={"text"} ref={refLogin} placeholder={"Введите логин"}></input>
                </div>
                <br></br>
                <div>
                    <input type={"password"} ref={refPassword} placeholder={"Введите пароль"}></input>
                    <input type={"password"} ref={refRePassword} placeholder={"Повторите пароль"}></input>
                </div>
                <br></br>
                <button onClick={handler}>Зарегестрироваться</button>
            </div>

    )
}