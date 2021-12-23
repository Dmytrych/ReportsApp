import React, {useEffect, useState} from 'react';
import { BrowserRouter, Route, Routes} from 'react-router-dom'
import LoginPage from "./components/LoginPage";
import {Box, Button} from "@mui/material";
import GenerateReportPage from "./components/GenerateReportPage";
import StudentManagePage from "./components/StudentManagePage";
import TopBar from "./components/TopBar";

function App() {
    const [isAuthenticated, setAuthenticated] = useState(false)
    const [userInfo, setUserInfo] = useState()

    if(!userInfo){
        let userInfoText = localStorage.getItem("userInfo")
        console.log(userInfoText)
        if (userInfoText){
            setUserInfo(JSON.parse(userInfoText))
            setAuthenticated(true)
        }
    }

    const unlogin = () => {
        setAuthenticated(false)
        localStorage.clear()
        deleteCookies()
    }

    const deleteCookies = () => {
        let cookies = document.cookie.split(";");

        for (let i = 0; i < cookies.length; i++) {
            let cookie = cookies[i];
            let eqPos = cookie.indexOf("=");
            let name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        }
    }

    return (
        <BrowserRouter>
            <Box sx={{display: 'flex', flexDirection: 'column', justifyContent: 'center'}}>
                <TopBar isAuthenticated={isAuthenticated} unlogin={unlogin}/>
                <Box>
                    <Routes>
                        <Route exact path='/login' element={<LoginPage setUserInfo={setUserInfo} setAuthenticated={setAuthenticated} isAuthenticated={isAuthenticated}/>} />
                        <Route exact path='/reports' element={<GenerateReportPage isAuthenticated={isAuthenticated} />} />
                        <Route exact path='/students' element={<StudentManagePage isAuthenticated={isAuthenticated} />} />
                    </Routes>
                </Box>
            </Box>
        </BrowserRouter>
    );
}

export default App;

