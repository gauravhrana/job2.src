IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailMappingUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailMappingUpdate'
	DROP  Procedure  ReleaseLogDetailMappingUpdate
END
GO

PRINT 'Creating Procedure ReleaseLogDetailMappingUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogDetailMappingUpdate
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ReleaseLogDetailMappingUpdate
(
		@ReleaseLogDetailMappingId		INT		 			
	,	@ChildReleaseLogDetailId		INT					
	,	@ParentReleaseLogDetailId		INT		
	,	@ApplicationId					INT			
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseLogDetailMapping'
)
AS
BEGIN 

	UPDATE	dbo.ReleaseLogDetailMapping 
	SET		ParentReleaseLogDetailId					=	@ParentReleaseLogDetailId
		,	ChildReleaseLogDetailId						=	@ChildReleaseLogDetailId	
		,	ApplicationId								=	@ApplicationId						
	WHERE	ReleaseLogDetailMappingId					=	@ReleaseLogDetailMappingId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseLogDetailMapping'
		,	@EntityKey				= @ReleaseLogDetailMappingId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO