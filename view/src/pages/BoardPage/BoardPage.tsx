import React, { useEffect, useState } from 'react'
import { BoardSection } from '../../components/BoardSection/BoardSection'
import styles from './BoardPage.module.scss'
import { sectionsEndpoints } from '../../Api/api-endpoints/sections-endponts'
import { useParams } from 'react-router-dom'
import { CreateSectionButton } from '../../components/CreateSectionButton/CreateSectionButton'
import { Loading } from '../../components/Loading/Loading'
import { noteEndpoints } from '../../Api/api-endpoints/note-endpoints'
import { DndProvider } from 'react-dnd'
import { HTML5Backend } from 'react-dnd-html5-backend'
import { boardsEndpoints } from '../../Api/api-endpoints/boards-endpoints'
import { BoardUsersModalW } from '../../components/BoardUsersModalW/BoardUsersModalW'

export const BoardPage = () => {
  
  const { id } = useParams();
  
  const { data: sectionsList, isLoading: isSectionsLoading, error: sectionError } = sectionsEndpoints.useGetBoardSectionsQuery(Number(id));
  const {data: boardData, isLoading : isBoardLoading, error: boardError} = boardsEndpoints.useGetBoardQuery(Number(id));

  const [createSection] = sectionsEndpoints.useCreateSectionMutation();
  const [createNote] = noteEndpoints.useCreateNoteMutation();

  const [deleteSection] = sectionsEndpoints.useDeleteSectionMutation();
  const [deleteNote] = noteEndpoints.useDeleteNoteMutation();

  const [updateNote] = noteEndpoints.useUpdateNoteMutation();
  const [updateSection] = sectionsEndpoints.useUpdateSectionMutation();
  const [updateBoard] = boardsEndpoints.useUpdateBoardMutation();

  const [currName, setName] = useState(boardData?.name);
  const [currDesc, setDesc] = useState(boardData?.description);

  useEffect(()=>{
    setName(boardData?.name);
    setDesc(boardData?.description);
  },[boardData])

  const isLoading = isSectionsLoading || !sectionsList || isBoardLoading || !boardData;

  const onDropHandler = ({curNoteId, curSecId, newSecId}:{curNoteId: number; curSecId:number; newSecId:number})=>{
    if (curSecId === newSecId)
      return
    updateNote({boardId:Number(id), sectionId:curSecId, noteId: curNoteId, updateName:'sectionId', updateData: newSecId});
  }

  if (isLoading)
    return <Loading />

  return (
    <DndProvider backend={HTML5Backend}>
    <div className='p-10'>
      <div className={styles.heading_div}>
        <input className={styles.name_input}
        value={currName}
        onChange={(e)=>setName(e.target.value)}
        onBlur={()=>updateBoard({boardId:Number(id), updateName:'name',updateData: currName as string})}/>
        <div>
        <CreateSectionButton boardId={Number(id)} createSectionMutation={(name) => createSection({ boardId: Number(id), secName: name })} />
        <BoardUsersModalW users={boardData.users} updateBoard={({updateName, updateData})=>updateBoard({boardId:Number(id), updateName:updateName, updateData:updateData})}/>
        </div>
        
      </div>
      <textarea className={styles.descr_input}
      value={currDesc}
      onChange={(e)=>setDesc(e.target.value)}
      onBlur={()=>updateBoard({boardId:Number(id), updateName:'description',updateData: currDesc as string})}
      />
      <div>
        {sectionsList.map(e=><BoardSection
        updateSection={({sectionId, newName})=>updateSection({boardId:Number(id), sectionId:sectionId, newName:newName})}
        updateNote={({sectionId, noteId, updateName, updateData})=>updateNote({boardId:Number(id), sectionId:sectionId, noteId:noteId, updateData:updateData, updateName:updateName})}
        onDropHandler={onDropHandler}
        deleteNote={({secId, noteId})=>deleteNote({boardId:Number(id), sectionId:secId, noteId:noteId})}
        sectionId={e.id} key={e.id} boardId={Number(id)} name={e.name} 
        deleteSection={(secId)=>deleteSection({boardId:Number(id), secId:secId})}
        createNote={({sectionId, name, description})=>createNote({boardId:Number(id), sectionId:sectionId, name:name, description:description})}/>)}
      </div>
      

    </div>
    </DndProvider>
  )
}
