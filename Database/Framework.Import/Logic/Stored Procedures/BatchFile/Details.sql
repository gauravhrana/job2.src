IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileDetails')
BEGIN
	PRINT 'Dropping Procedure BatchFileDetails'
	DROP  Procedure BatchFileDetails
END
GO

PRINT 'Creating Procedure BatchFileDetails'
GO


/******************************************************************************
**		File: 
**		Name: BatchFileDetails
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

CREATE Procedure dbo.BatchFileDetails
(
		@BatchFileId		INT							
	,	@AuditId			INT							
	,	@AuditDate			DATETIME	= NULL			
	,	@SystemEntityType	VARCHAR(50)	= 'BatchFile'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@BatchFileId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.BatchFileId	
		,	a.ApplicationId	
		,	a.Name				
		,	a.Folder				
		,	a.BatchFile
		,	a.BatchFileSetId			
		,	a.Description		
		,	a.FileTypeId			
		,	a.SystemEntityTypeId	
		,	a.BatchFileStatusId		
		,	a.CreatedDate		
		,	a.CreatedByPersonId	
		,	a.UpdatedDate		
		,	a.UpdatedByPersonId
		,	a.Errors	
		,	g.Name							AS 'BatchFileSet'
		,	c.EntityName					AS 'SystemEntityType'
		,	b.Name							AS 'FileType'
		,	f.Name							AS 'BatchFileStatus'
		,	d.FirstName + ' ' + d.LastName	AS 'CreatedByPerson'
		,	e.FirstName + ' ' + e.LastName	AS 'UpdatedByPerson'
		,	@LastUpdatedDate				AS	'Updated Date'
		,	@LastUpdatedBy					AS	'Updated By'
		,	@LastAuditAction				AS	'Last Action'							
	FROM	dbo.BatchFile a
			INNER JOIN dbo.FileType			b		ON		a.FileTypeId			= b.FileTypeId
			INNER JOIN dbo.BatchFileStatus	f		ON		a.BatchFileStatusId		= f.BatchFileStatusId
			INNER JOIN dbo.SystemEntityType	c		ON		a.SystemEntityTypeId	= c.SystemEntityTypeId
			INNER JOIN dbo.BatchFileSet		g		ON		a.BatchFileSetId		= g.BatchFileSetId
			INNER JOIN dbo.Person			d		ON		a.CreatedByPersonId		= d.PersonId	
			INNER JOIN dbo.Person			e		ON		a.UpdatedByPersonId		= e.PersonId
	WHERE	BatchFileId = @BatchFileId	
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO
   