IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogUpdate')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogUpdate'
	DROP  Procedure  StoredProcedureLogUpdate
END
GO

PRINT 'Creating Procedure StoredProcedureLogUpdate'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedureLogUpdate
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.StoredProcedureLogUpdate
(
		@StoredProcedureLogId	INT			= NULL 	OUTPUT		
	,	@Name					VARCHAR(50)						
	,	@TimeOfExecution		VARCHAR(50)						
	,	@ExecutedBy				INT								
	,	@AuditId				INT								
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'StoredProcedureLog'
)
AS
BEGIN 

	UPDATE	dbo.StoredProcedureLog 
	SET		Name					= @Name
		,	TimeOfExecution			= @TimeOfExecution
		,   ExecutedBy				= @ExecutedBy
	WHERE	StoredProcedureLogId	= @StoredProcedureLogId

	

 END		
 GO