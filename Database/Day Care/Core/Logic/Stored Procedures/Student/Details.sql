IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StudentDetails')
BEGIN
	PRINT 'Dropping Procedure StudentDetails'
	DROP  Procedure StudentDetails
END
GO

PRINT 'Creating Procedure StudentDetails'
GO


/******************************************************************************
**		File: 
**		Name: Student_Details
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

CREATE Procedure dbo.StudentDetails
(
		@StudentId		         INT
	,	@ApplicationId			 INT
	,   @AuditId				 INT			
    ,   @AuditDate				 DATETIME		= NULL
	,   @SystemEntityType		 VARCHAR(50)	= 'Student'
)
	
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@StudentId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	StudentId
		,	ApplicationId
		,	FirstName
		,   LastName
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'										
	FROM	Student 
	WHERE	StudentId	  = @StudentId	
	AND		ApplicationId = @ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Student'
		,	@EntityKey				= @StudentId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO
   