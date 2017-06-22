IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogStatusClone')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogStatusClone'
	DROP  Procedure ReleaseLogStatusClone
END
GO

PRINT 'Creating Procedure ReleaseLogStatusClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleaseLogStatusClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/
CREATE Procedure dbo.ReleaseLogStatusClone
(
		@ReleaseLogStatusId		        INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)						
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseLogStatus'
)
AS
BEGIN
		IF @ReleaseLogStatusId IS NULL OR @ReleaseLogStatusId = -999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseLogStatusId OUTPUT
		END						
	
		SELECT	@ApplicationId	= ApplicationId
			,	@Description	= Description
			,	@SortOrder		= SortOrder				
		FROM	dbo.ReleaseLogStatus
		WHERE   ReleaseLogStatusId		= @ReleaseLogStatusId

		EXEC dbo.ReleaseLogStatusInsert 
			@ReleaseLogStatusId		=	NULL
		,	@ApplicationId			=	@ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseLogStatus'
		,	@EntityKey				= @ReleaseLogStatusId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

	END	
GO
