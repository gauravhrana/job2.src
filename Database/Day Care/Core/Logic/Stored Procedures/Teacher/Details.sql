IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TeacherDetails')
BEGIN
	PRINT 'Dropping Procedure TeacherDetails'
	DROP  Procedure TeacherDetails
END
GO

PRINT 'Creating Procedure TeacherDetails'
GO

/******************************************************************************
**		File: 
**		Name: TeacherDetails
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

CREATE Procedure dbo.TeacherDetails
(
		@TeacherId		    INT
	,	@ApplicationId		INT		
	,   @AuditId            INT		
    ,   @AuditDate          DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Teacher'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TeacherId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	TeacherId
		,	ApplicationId
		,	LastName
		,	FirstName
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'									
	FROM	Teacher 
	WHERE	TeacherId		= @TeacherId	
	AND 	ApplicationId	= @ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Teacher'
		,	@EntityKey				= @TeacherId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO
   