import { MenuItem } from "@material-ui/core";
import React from "react";
import { sendReq } from "../../Api/Auth/sendReq";
export const LeaveFromChat=({guid})=>{

    const leave=()=>{
        sendReq("post", "api/chat/LeaveFromChat", {data:{
            guid:guid
        },
        headers:{
            'Content-Type': 'application/json'
        }
        });
    }

    return (
        <MenuItem onClick={leave}>Leave</MenuItem>
    )
}