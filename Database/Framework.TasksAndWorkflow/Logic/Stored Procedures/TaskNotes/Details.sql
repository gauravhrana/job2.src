IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskNotesDetails')
BEGIN
	PRINT 'Dropping Procedure TaskNotesDetails'
	DROP  Procedure TaskNotesDetails
END
GO

PRINT 'Creating Procedure TaskNotesDetails'
GO


/******************************************************************************
**		File: 
**		Name: TaskNotesDetails
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

CREATE Procedure dbo.TaskNotesDetails
(
		@TaskNotesId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TaskNotes'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TaskNotesId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.TaskNotesId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.Description		 
		,	a.SortOrder
		,	@LastUpdatedDate		AS	'Updated Date'
		,	@LastUpdatedBy			AS	'Updated By'
		,	@LastAuditAction		AS	'Last Action'
	FROM		dbo.TaskNotes a
	
	WHERE	a.TaskNotesId	= @TaskNotesId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskNotesId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   