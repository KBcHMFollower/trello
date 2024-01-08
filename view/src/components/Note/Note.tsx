import React, { FC, useState } from 'react'
import styles from './Note.module.scss'
import { useDrag } from 'react-dnd';
import { onFocusLost } from '@reduxjs/toolkit/dist/query/core/setupListeners';

type PropsType = {
  name:string;
  noteId:number;
  sectionId:number;
  description:string;
  deleteNote: (noteId:number)=>void;
  updateNote: (data: {noteId:number; updateName:string; updateData:string})=>void;
}

export const Note:FC<PropsType> = ({name, description, deleteNote, noteId, sectionId, updateNote}) => {


  console.log('update')
  const [,dragItem] = useDrag(()=>({
    type:'NOTE',
    item:{noteId:noteId, secId:sectionId},
    collect: (monitor) => ({
      opacity: monitor.isDragging() ? 0.5 : 1
    }),
    shouldStartDrag: (event:any) => ((event.target.nodeType !== Node.TEXT_NODE) && (event.target.tagName.toLowerCase() !== 'input')),
  }),[])

  const [currName, setName] = useState(name);
  const [currDescr, setDescr] = useState(description);

  return (
    <div
    ref={dragItem}
    draggable
     className={styles.main_div}>
        <input className={styles.heading}
        value={currName}
        onChange={(e)=>setName(e.target.value)}
        onBlur={()=>updateNote({noteId:noteId, updateName:'name', updateData:currName})}
        />
        <textarea className={styles.description}
        value={currDescr}
        onChange={e=>setDescr(e.target.value)}
        onBlur={()=>updateNote({noteId:noteId, updateName:'description', updateData:currDescr})}/>
        <button
        onClick={()=>deleteNote(noteId)}
         className={styles.delete_button}>delete</button>
    </div>
  )
}
