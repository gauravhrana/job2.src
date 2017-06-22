 IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BathroomDetails')
BEGIN
	PRINT 'Dropping Procedure BathroomDetails'
	DROP  Procedure BathroomDetails
END
GO

PRINT 'Creating Procedure BathroomDetails'
GO


/******************************************************************************
**		File: 
**		Name: BathroomDetails
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

CREATE Procedure dbo.BathroomDetails
(
		@BathroomId		    INT     
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Bathroom'	
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@BathroomId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	BathroomId
		,	ApplicationId
		,	StudentId
		,	TimeIn
		,	DiaperStatusId
		,	DiaperCream
		,	PottyStatus
		,	TeacherId
		,	TeacherNotes	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'								
	FROM	dbo.Bathroom 
	WHERE	BathroomId = @BathroomId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @BathroomId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 	
END		
GO
   