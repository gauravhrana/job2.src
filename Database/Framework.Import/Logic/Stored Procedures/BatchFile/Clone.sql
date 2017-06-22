IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileClone')
BEGIN
	PRINT 'Dropping Procedure BatchFileClone'
	DROP  Procedure BatchFileClone
END
GO

PRINT 'Creating Procedure BatchFileClone'
GO

/*********************************************************************************************
**		File: 
**		Name: BatchFileClone
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

CREATE Procedure dbo.BatchFileClone
(		
		@BatchFileId			INT		 		= NULL 	OUTPUT	
	,	@ApplicationId			INT				= NULL
	,	@Name					VARCHAR(50)						 
	,	@Folder					VARCHAR(1000)
	,	@BatchFile				VARCHAR(150)
	,	@BatchFileSetId			INT					 
	,	@Description			VARCHAR(150)	= NULL			
	,	@FileTypeId				INT					 
	,	@SystemEntityTypeId		INT								
	,	@BatchFileStatusId		INT						
	,	@CreatedDate			DATETIME						
	,	@CreatedByPersonId		INT								
	,	@UpdatedDate			DATETIME		= NULL			
	,	@UpdatedByPersonId		INT				= NULL			
	,	@Errors					VARCHAR(1000)	= NULL	
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL			
	,	@SystemEntityType	    VARCHAR(50)	    ='BatchFile'	
)
AS
BEGIN		
	
	SELECT	@Name					=		Name	
		,	@ApplicationId			=		ApplicationId			
		,	@Folder					=		Folder				
		,	@BatchFile				=		BatchFile			
		,	@BatchFileSetId			=		BatchFileSetId			
		,	@Description			=		Description			
		,	@FileTypeId				=		FileTypeId			
		,	@SystemEntityTypeId		=		SystemEntityTypeId	
		,	@BatchFileStatusId		=		BatchFileStatusId		
		,	@CreatedDate			=		CreatedDate			
		,	@CreatedByPersonId		=		CreatedByPersonId	
		,	@UpdatedDate			=		UpdatedDate		
		,	@UpdatedByPersonId		=		UpdatedByPersonId	
		,	@Errors					=		Errors						
	FROM	dbo.BatchFile
	WHERE	BatchFileId	= @BatchFileId

	EXEC	dbo.BatchFileInsert 
			@BatchFileId			=	NULL
		,	@ApplicationId			=	@ApplicationId
		,	@Name					=	@Name				
		,	@Folder					=	@Folder				
		,	@BatchFile				=	@BatchFile
		,	@BatchFileSetId			=	@BatchFileSetId			
		,	@Description			=	@Description		
		,	@FileTypeId				=	@FileTypeId			
		,	@SystemEntityTypeId		=	@SystemEntityTypeId	
		,	@BatchFileStatusId		=	@BatchFileStatusId		
		,	@CreatedDate			=	@CreatedDate		
		,	@CreatedByPersonId		=	@CreatedByPersonId	
		,	@UpdatedDate			=	@UpdatedDate		
		,	@UpdatedByPersonId		=	@UpdatedByPersonId
		,	@Errors					=	@Errors	
									
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@BatchFileId
		,	@AuditAction			=	'Clone'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId	

END	
GO
