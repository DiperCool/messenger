import React, { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { UserContext } from "../UserComponent/UserContext";

export const GetLogin=()=>{

    let [login,setLogin]= useState("");
    let [isR,setR]= useState(false);
    let {Auth}= useContext(UserContext);
    useEffect(()=>{
        Auth.con.on("ReceiveMessege", (room, message)=>{
            console.log(room, message)
        })
        console.log("sdsd")
        Auth.con.invoke("SendMessege", {"guid":"b5ccce39-3ceb-45ea-8ccb-b04984e2113f"}, {"Content":"вф"})
    },[])
    return(
        <div>
            {login}
            <Link to="/login">хуй</Link>
            <button onClick={()=>{setR(true)}}>Получить</button>
        </div>
    )
}