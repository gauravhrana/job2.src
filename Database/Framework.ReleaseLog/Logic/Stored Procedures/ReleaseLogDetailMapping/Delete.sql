IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailMappingDelete')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailMappingDelete'
	DROP  Procedure ReleaseLogDetailMappingDelete
END
GO

PRINT 'Creating Procedure ReleaseLogDetailMappingDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReleaseLogDetailMappingDelete
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
CREATE Procedure dbo.ReleaseLogDetailMappingDelete
(
		@ReleaseLogDetailMappingId 		INT			= NULL		
	,	@ParentReleaseLogDetailId 		INT			= NULL		
	,	@ChildReleaseLogDetailId 		INT			= NULL		
	,	@AuditId						INT						
	,	@AuditDate						DATETIME	= NULL		
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseLogDetailMapping'
)
AS
BEGIN

	DELETE	dbo.ReleaseLogDetailMapping
	WHERE	ReleaseLogDetailMappingId			=	ISNULL(@ReleaseLogDetailMappingId,	ReleaseLogDetailMappingId)	
	AND		ParentReleaseLogDetailId		=	ISNULL(@ParentReleaseLogDetailId,			ParentReleaseLogDetailId)
	AND		ChildReleaseLogDetailId		=	ISNULL(@ChildReleaseLogDetailId,			ChildReleaseLogDetailId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseLogDetailMapping'
		,	@EntityKey				= @ReleaseLogDetailMappingId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
