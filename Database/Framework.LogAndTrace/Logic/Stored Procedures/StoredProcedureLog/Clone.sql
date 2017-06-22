IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogClone')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogClone'
	DROP  Procedure StoredProcedureLogClone
END
GO

PRINT 'Creating Procedure StoredProcedureLogClone'
GO

/*********************************************************************************************
**		File: 
**		Name: StoredProcedureLogClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.StoredProcedureLogClone
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
	
	SELECT	@Name					= Name
		,	@TimeOfExecution		= TimeOfExecution
		,   @ExecutedBy				= ExecutedBy
				
	FROM	dbo.StoredProcedureLog
	WHERE   StoredProcedureLogId	= @StoredProcedureLogId

	EXEC dbo.StoredProcedureLogInsert 
			@StoredProcedureLogId		=	NULL
		,	@Name						= @Name
		,	@TimeOfExecution			= @TimeOfExecution
		,   @ExecutedBy					= @ExecutedBy



END	
GO
