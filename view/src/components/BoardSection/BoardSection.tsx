import React, { FC, useState } from 'react'
import styles from './BoardSection.module.scss'
import { Note } from '../Note/Note'
import { CreateNoteButton } from '../CreateNoteButton/CreateNoteButton';
import { noteEndpoints } from '../../Api/api-endpoints/note-endpoints';
import { useDrop } from 'react-dnd';

type PropsType = {
    name: string;
    boardId: number;
    sectionId: number;
    deleteSection: (secId: number) => void;
    updateNote: (data: { noteId: number; sectionId: number; updateName: string; updateData: string }) => void
    deleteNote: ({ secId, noteId }: { secId: number; noteId: number }) => void;
    createNote: ({ name, description, sectionId }: { name: string; description: string, sectionId: number }) => void;
    onDropHandler: ({ curNoteId, curSecId, newSecId }: { curNoteId: number; curSecId: number; newSecId: number }) => void;
    updateSection: (data:{sectionId:number; newName:string})=>void;
}

export const BoardSection: FC<PropsType> = ({ name, updateSection, createNote, updateNote, sectionId, boardId, deleteSection, deleteNote, onDropHandler }) => {

    const { data, isLoading } = noteEndpoints.useGetSectionsNotesQuery({ boardId: boardId, sectionId: sectionId })

    const [currName, setName] = useState(name);

    const [, dropableArea] = useDrop({
        accept: 'NOTE',
        drop: (item: { noteId: number, secId: number }) => {
            console.log("drop");
            onDropHandler({ curNoteId: item.noteId, curSecId: item.secId, newSecId: sectionId });
        }
    })

    if (!data || isLoading)
        return (
            <div className={styles.main_div}>
                <div className={styles.header_div}>
                    <h3 className=' font-medium'>{name}</h3>
                    <button className={styles.delete_button}>X</button>
                </div>
                <Note
                    updateNote={({ noteId, updateName, updateData }) => updateNote({ sectionId: sectionId, noteId: noteId, updateName: updateName, updateData: updateData })}
                    sectionId={sectionId}
                    noteId={-1}
                    deleteNote={(noteId) => deleteNote({ secId: sectionId, noteId: noteId })}
                    name='Section is loading' description='Please, wait one sec' />
            </div>
        )

    return (
        <div
            ref={dropableArea}
            className={styles.main_div}>
            <div className={styles.header_div}>
                <input className={styles.heading}
                value={currName}
                onChange={(e)=>setName(e.target.value)}
                onBlur={()=>updateSection({sectionId:sectionId, newName:currName})}/>
                <button
                    onClick={() => deleteSection(sectionId)}
                    className={styles.delete_button}>X</button>
            </div>
            {data.map(e => <Note
                updateNote={({ noteId, updateName, updateData }) => updateNote({ sectionId: sectionId, noteId: noteId, updateName: updateName, updateData: updateData })}
                sectionId={sectionId}
                noteId={e.id}
                deleteNote={(noteId) => deleteNote({ secId: sectionId, noteId: noteId })}
                key={e.id} name={e.name} description={e.description} />)}
            <CreateNoteButton createNoteMutation={({ name, description }) => createNote({ name: name, description: description, sectionId: sectionId })} />
        </div>
    )
}
