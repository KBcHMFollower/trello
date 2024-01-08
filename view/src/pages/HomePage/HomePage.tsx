import React from 'react'
import { CreateBoardButton } from '../../components/CreateBoardButton/CreateBoardButton'
import { BoardCard } from '../../components/BoardCard/BoardCard'
import { useAppSelector } from '../../hooks/redux'
import { boardsEndpoints } from '../../Api/api-endpoints/boards-endpoints'
import { Loading } from '../../components/Loading/Loading'

export const HomePage = () => {

  const {id} = useAppSelector(state=>({
    id:state.user.user.id
  }));

  const {data:boardsList, isLoading: isBoardsLoading, error} = boardsEndpoints.useGetUserBoardsQuery(id);

  const [createBoard] = boardsEndpoints.useCreateBoardMutation();

  const [updateBoard] = boardsEndpoints.useUpdateBoardMutation();

  const isLoading = !boardsList || isBoardsLoading;

  if (isLoading)
    return (
      <Loading/>
  )

  return (
    <div className='flex justify-start p-5 gap-5 flex-row flex-wrap items-start content-start'>
      
      <CreateBoardButton userId={id} createBoardMutation={({userId, name, description})=>createBoard({userId, name, description})}/>
      {boardsList.map(e=><BoardCard
      updateBoard={({boardId, updateName, updateData})=>updateBoard({boardId:boardId, updateData:updateData,  updateName:updateName})}
       id={e.id} name={e.name} description={e.description} userId={id} key={e.id}/>)}
    </div>
  )
}
