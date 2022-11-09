import React from 'react'

const Response = (props) => (
    <div style={{ background: "#eee", borderRadius: '5px', padding: '0 10px', color: 'black'}}>
    <p> &gt;&gt; {props.input}</p>
    <p>{props.output}</p>
    <p>{props.error}</p>
</div>
)

export default Response;
