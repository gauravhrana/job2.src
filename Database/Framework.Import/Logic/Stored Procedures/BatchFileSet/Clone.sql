IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileSetClone')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetClone'
	DROP  Procedure BatchFileSetClone
END
GO

PRINT 'Creating Procedure BatchFileSetClone'
GO

/*********************************************************************************************
**		File: 
**		Name: BatchFileSetClone
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

CREATE Procedure dbo.BatchFileSetClone
(
		@BatchFileSetId			INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			= NULL	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@CreatedDate			DATETIME						
	,	@CreatedByPersonId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'BatchFileSet'				
)
AS
BEGIN		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description
		,	@CreatedDate		= CreatedDate
		,	@CreatedByPersonId	= CreatedByPersonId				
	FROM	dbo.BatchFileSet
	WHERE	BatchFileSetId		= @BatchFileSetId

	EXEC dbo.BatchFileSetInsert 
			@BatchFileSetId		=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@CreatedDate		=	@CreatedDate
		,	@CreatedByPersonId	=	@CreatedByPersonId
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileSetId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
