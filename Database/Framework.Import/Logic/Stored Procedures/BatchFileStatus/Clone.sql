IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileStatusClone')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusClone'
	DROP  Procedure BatchFileStatusClone
END
GO

PRINT 'Creating Procedure BatchFileStatusClone'
GO

/*********************************************************************************************
**		File: 
**		Name: BatchFileStatusClone
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

CREATE Procedure dbo.BatchFileStatusClone
(
		@BatchFileStatusId		INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			= NULL	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'BatchFileStatus'				
)
AS
BEGIN	
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.BatchFileStatus
	WHERE	BatchFileStatusId		= @BatchFileStatusId

	EXEC dbo.BatchFileStatusInsert 
			@BatchFileStatusId	=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileStatusId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
