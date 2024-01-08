import React, { FC, useState } from 'react'
import styles from './CreateSectionButton.module.scss'


type PropsType = {
    boardId:number;
    createSectionMutation : (name:string)=>void;
}

export const CreateSectionButton:FC<PropsType> = ({createSectionMutation, boardId}) => {

    const [isOpen, setIsOpen] = useState(false);

    const [name, setName] = useState('');

    const onCreateHandler = ()=>{
        createSectionMutation(name);
        setName('');
        setIsOpen(false);
    }

    return (
        <>
        <button
        onClick={()=>setIsOpen(!isOpen)} 
        className={styles.open_button}>
            Create section
        </button>

        <div className={`${styles.modal} ${isOpen && styles.active}`}>
            <h2 className='font-bold'>Create Section</h2>
            <input
            onChange={(e)=>setName(e.target.value)}
            value={name}
             type="text" placeholder='Name' required className={styles.name_input} />
            <button
            onClick={onCreateHandler}
             className={styles.create_button}>Create</button>
        </div>
        <div
        onClick={()=>setIsOpen(!isOpen)}
         className={`${styles.overlay} ${isOpen && styles.active}`}></div>
        </>
        
    )
}
