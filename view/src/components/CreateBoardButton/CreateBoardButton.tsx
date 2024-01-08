import React, { FC, useState } from 'react'
import styles from './CreateBoardButton.module.scss'


type PropsType = {
    userId:number;
    createBoardMutation : ({userId, name, description}:{userId: number; name:string; description:string})=>void;
}

export const CreateBoardButton:FC<PropsType> = ({createBoardMutation, userId}) => {

    const [isOpen, setIsOpen] = useState(false);

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');

    const onCreateHandler = ()=>{
        createBoardMutation({
            userId : userId,
            name: name,
            description: description
        })
        setName('');
        setDescription('');
        setIsOpen(false);
    }

    return (
        <>
        <button
        onClick={()=>setIsOpen(!isOpen)} 
        className={styles.open_button}>
            <div className={styles.button_div}>
                <p className='text-blue-800 self-center'>Create Board</p>
            </div>
        </button>

        <div className={`${styles.modal} ${isOpen && styles.active}`}>
            <h2 className='font-bold'>Create Board</h2>
            <input
            onChange={(e)=>setName(e.target.value)}
            value={name}
             type="text" placeholder='Name' required className={styles.name_input} />
            <input
            onChange={(e)=>setDescription(e.target.value)}
            value={description}
             type="text" placeholder='Description' className={styles.name_input} />
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
