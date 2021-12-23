import {useEffect, useState} from "react";
import {Button, Paper} from '@mui/material';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import {useNavigate} from "react-router-dom";

export default function LoginPage({setUserInfo, setAuthenticated, isAuthenticated}) {
    const navigate = useNavigate()
    const [login, setLogin] = useState("");
    const [password, setPassword] = useState("");

    useEffect(() => {
        if(isAuthenticated){
            navigate("/reports")
        }
    })

    let handleLoginOnChange = event => {
      setLogin(event.target.value)
    };

    let handlePasswordOnChange = event => {
        setPassword(event.target.value)
      };

    let handleSubmit = async () => {
        if (login && password){
            let response = await fetch('http://localhost:8080/AuthApi/Login', {
                mode: 'cors',
                method: 'POST',
                headers: {
                    'Content-Type': 'text/plain'
                },
                credentials: "include",
                body: JSON.stringify({Login: login, Password: password})
            }).catch(ex => console.log(ex))

            if (response && response.ok){
                let userInfo = await response.json()
                localStorage.setItem("userInfo", JSON.stringify(userInfo))
                setAuthenticated(true)
                navigate("/reports")
                return;
            }
            alert("Invalid Credentials")
        }
    }
    
    return (
        <Box sx={{display: 'flex', justifyContent: 'center'}}>
            <Paper sx={{width: '10rem', display: 'flex', flexDirection: 'column', justifyContent: 'center'}}>
                <Box>
                    <TextField onChange={handleLoginOnChange} label="Login" value={login}/>
                </Box>
                <Box>
                    <TextField onChange={handlePasswordOnChange} label="Password" value={password}/>
                </Box>
                <Box>
                    <Button onClick={handleSubmit}>Submit</Button>
                </Box>
            </Paper>
        </Box>
    )
}