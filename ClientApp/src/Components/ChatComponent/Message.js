import { Avatar, Grid } from '@material-ui/core';
import React, { useContext } from 'react';
import { UserContext } from '../UserComponent/UserContext';
import "./style.css"
export const Message= React.memo(({guid,creator, content, timeStamp})=>{

    const {Auth}= useContext(UserContext)
    const xuy=Auth.login===creator.login;

    return (
        <div>
            
            <div className={`message-${xuy?"orange":"blue"}`}>
                {xuy?null:
                <div>
                    <Grid container alignItems="center" spacing={1}>
                        <Grid item>
                            <Avatar src={""} style={{display: 'inline-block'}} />
                        </Grid>
                        <Grid item>
                            <b>{creator.login}</b>
                        </Grid>
                    </Grid>
                </div>}
                <p className="message-content">{content}</p>
                <br></br>
                <div className={`message-timestamp-${xuy?"right":"left"}`}>{getTime(timeStamp)}</div>
            </div>
        </div>
    )
});

const getTime=time=>{
    var e=new Date(time);
    return (e.getHours()+":"+(e.getMinutes().toString().length===1?"0"+e.getMinutes():e.getMinutes()));
}