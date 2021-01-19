import { Button, TextField } from '@material-ui/core';
import React, { useEffect, useRef, useState } from 'react';
import { sendReq } from '../../Api/Auth/sendReq';
import { Message } from "./Message";
export const ChatView = ({ guid, messages, setNewMessages }) => {

    let [isVisible, setVisible] = useState(true);
    let textRef = useRef(null);

    useEffect(() => {
        setVisible(true);
    }, [guid])


    const newMessages = () => {
        let text = textRef.current.value;
        let formData = new FormData();
        formData.append("guid", guid);
        formData.append("Content", text);
        console.log(guid);
        textRef.current.value = null;
        sendReq("post", "api/chat/CreateMessage", {
            data: formData, headers: {
                'Content-Type': 'multipart/form-data'
            }
        })

    }

    const GetMessages = async () => {
        let id = messages[0].id;
        let res = await sendReq("get", `api/chat/getMessages?guid=${guid}&id=${id}`);
        if (res.data.length === 0) {
            setVisible(false);
        }
        setNewMessages(guid, res.data);
    }
    return (
        <div>
            <div
                style={{ height: "83vh", overflow: "scroll", overflowX: "hidden" }}
                direction="column"
                justify="flex-end"

            >
                {isVisible ? <button onClick={GetMessages}>еще</button> : null}
                {messages.map((el) => <Message key={el.id} guid={guid} content={el.content} creator={el.creator} timeStamp={el.created} />)}
            </div>
            <TextField type={"text"} placeholder={"Enter a text"} inputRef={textRef} style={{ width: "80%" }} />
            <Button style={{ width: "20%" }} onClick={newMessages}>Send</Button>
        </div>
    )
}

// [Required]
// public string Content{get;set;}
// public List<IFormFile> Files= new List<IFormFile>();
// [Required]
// public string guid{get;set;}