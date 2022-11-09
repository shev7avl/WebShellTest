import React, { useState, useEffect, useRef } from 'react';
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import axios from 'axios';
import ConsoleInput from "./ConsoleInput";
import ConsoleWindow from "./ConsoleWindow";

const Shell = () => {
  const [connection, setConnection] = useState(null);
  const [shell, setShell] = useState([]);
  const latestShell = useRef(null);

  latestShell.current = shell;

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
    .withUrl('https://localhost:5001/shellHub')
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Information)
    .build();

      console.log(newConnection.baseUrl);
      console.log(newConnection.connectionId);
      setConnection(newConnection);

  }, []);

  useEffect(() => {
    if(connection){
      connection
      .start()
      .then(result => {
          console.log('Connected!');

          connection.on('ReceiveRequestResult', request => {
            const updatedShell = [...latestShell.current];
            updatedShell.push(request);

            setShell(updatedShell); 
        });
      })
      .catch(e => console.log('Connection failed: ', e));
    }}, [connection]);

    const sendRequest = async (input) => {
      let request = {
        input : input,
        output: null,
        error: null,
      }
      console.log("sending request: ", request.input);     
      console.log("Connection status: ", connection);   

        try {
              const response = await fetch('https://localhost:5001/api/v1/ShellRequest/rq:'+input, {
                  method: 'POST',
                  headers: {
                      'Content-Type': 'application/json',
                      'Charset': 'utf-8'
          }
        
        });
        
          if(response.ok)
          {
            let result = await response.json();
            request.output = result.output;
            request.error = result.error;
            
            console.log('Request input', request.input);
            console.log('Request output', request.output);
            console.log('Request error', request.error);
          }
          }
          catch(e) {
              console.log('Sending request failed.', e);
          }
        };

    const selectInput = () => {

    }
    
        return (
          <div>
              <ConsoleWindow shell={shell}/>
              <hr />
              <ConsoleInput sendRequest={sendRequest} />
          </div>
      );
  
  }
  

export default Shell;
