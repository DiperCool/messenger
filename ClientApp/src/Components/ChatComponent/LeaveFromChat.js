import { MenuItem } from "@material-ui/core";
import React from "react";
import { sendReq } from "../../Api/Auth/sendReq";
export const LeaveFromChat=({guid,handleClose})=>{

    const leave=()=>{
        sendReq("post", "api/chat/LeaveFromChat", {data:{
            guid:guid
        },
        headers:{
            'Content-Type': 'application/json'
        }
        });
        handleClose();
    }

    return (
        <MenuItem onClick={leave}>Leave</MenuItem>
    )
}