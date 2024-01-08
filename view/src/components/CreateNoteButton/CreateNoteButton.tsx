import React, { FC, useState } from 'react'
import styles from './CreateNoteButton.module.scss'


type PropsType = {
    createNoteMutation : ({name, description}:{name:string; description:string})=>void;
}

export const CreateNoteButton:FC<PropsType> = ({createNoteMutation}) => {

    const [isOpen, setIsOpen] = useState(false);

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');

    const onCreateHandler = ()=>{
        createNoteMutation({
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
            Create Note
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
